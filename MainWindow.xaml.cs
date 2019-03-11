using MMA_WorkFinder.Structs;
using NSoup.Nodes;
using NSoup.Select;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;

namespace MMA_WorkFinder
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            //dynamic activeX = this.ViewPage.GetType().InvokeMember("ActiveXInstance",
            //        BindingFlags.GetProperty | BindingFlags.Instance | BindingFlags.NonPublic,
            //        null, this.ViewPage, new object[] { });
            //activeX.Silent = true;

            ViewPage.NavigateToString(Formatter.GetHtml("<div class=\"layer\"><div class=\"layer_inner\"><img class=\"content\" draggable=\"false\" src=\"https://www.mma.go.kr/download/content/usr0000248/img3.gif\"><div></div>"));
        }

        private List<CoData> coList = new List<CoData>();

        private void SearchQueryInput_KeyUp(object sender, KeyEventArgs e)
        {
            if (!SearchQueryInput.Text.Equals(string.Empty) && e.Key.Equals(Key.Enter))
            {
                coList.Clear();
                ResultListView.Items.Clear();
                string result = Network.Request("https://work.mma.go.kr/caisBYIS/search/byjjecgeomsaek.do?eopjong_gbcd=" + (PositionSelector.SelectedIndex + 1) + "&eopche_nm=" + SearchQueryInput.Text);
                Document doc = NSoup.Parse.Parser.Parse(result, "https://work.mma.go.kr");
                Elements elms = doc.Select("th.title.t-alignLt.pl20px");
                foreach (var item in elms)
                {
                    CoData coData = new CoData();
                    coData.SetName(item.Text());
                    coData.SetId(GetMiddleString(item.Select("a").Attr("href"), "byjjeopche_cd=", "&"));
                    coList.Add(coData);
                    ResultListView.Items.Add(coData.GetName() + Environment.NewLine + "기업코드: " + coData.GetId());
                }
            }
        }

        private void PositionSelector_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            try
            {
                if (!SearchQueryInput.Text.Equals(string.Empty))
                {
                    coList.Clear();
                    ResultListView.Items.Clear();
                    string result = Network.Request("https://work.mma.go.kr/caisBYIS/search/byjjecgeomsaek.do?eopjong_gbcd=" + (PositionSelector.SelectedIndex + 1) + "&eopche_nm=" + SearchQueryInput.Text);
                    Document doc = NSoup.Parse.Parser.Parse(result, "https://work.mma.go.kr");
                    Elements elms = doc.Select("th.title.t-alignLt.pl20px");
                    foreach (var item in elms)
                    {
                        CoData coData = new CoData();
                        coData.SetName(item.Text());
                        coData.SetId(GetMiddleString(item.Select("a").Attr("href"), "byjjeopche_cd=", "&"));
                        coList.Add(coData);
                        ResultListView.Items.Add(coData.GetName() + Environment.NewLine + "기업코드: " + coData.GetId());
                    }
                }
            }
            catch { }
        }

        public static string GetMiddleString(string str, string begin, string end)
        {
            if (string.IsNullOrEmpty(str))
            {
                return null;
            }

            string result = null;
            if (str.IndexOf(begin) > -1)
            {
                str = str.Substring(str.IndexOf(begin) + begin.Length);
                if (str.IndexOf(end) > -1) result = str.Substring(0, str.IndexOf(end));
                else result = str;
            }
            return result;
        }

        private void ResultListView_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            try
            {
                string result = Network.Request("https://work.mma.go.kr/caisBYIS/search/byjjecgeomsaekView.do?byjjeopche_cd=" + coList[ResultListView.SelectedIndex].GetId());
                Document doc = NSoup.Parse.Parser.Parse(result, "https://work.mma.go.kr");
                doc.Select("table").RemoveClass("table_row").AddClass("table table-bordered table-hover");
                doc.Select("caption").Remove();
                doc.Select("div.blist_btn_n").Remove();
                string html = doc.Select("div[id=content]").OuterHtml();
                ViewPage.NavigateToString(Formatter.GetHtml(html + "<center>(우)35208 대전광역시 서구 청사로 189, 정부대전청사 2동 대표전화 : 1588-9090<br>COPYRIGHT(C) Military Manpower Administration ALL RIGHTS RESERVED.</center>"));
            }
            catch { }
        }
    }
}