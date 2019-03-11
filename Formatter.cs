namespace MMA_WorkFinder
{
    internal class Formatter
    {
        public static string GetHtml(string raw)
        {
            string html = null;
            AddLine(ref html, "<html><head><meta charset=\"utf-8\"><meta http-equiv=\"X-UA-Compatible\" content=\"IE=edge\" />");

            // Bootstrap
            AddLine(ref html, "<meta name=\"viewport\" content=\"width=device-width, initial-scale=1\">");
            AddLine(ref html, "<link rel=\"stylesheet\" href=\"https://maxcdn.bootstrapcdn.com/bootstrap/3.4.0/css/bootstrap.min.css\">");
            AddLine(ref html, "<script src=\"https://ajax.googleapis.com/ajax/libs/jquery/3.3.1/jquery.min.js\"></script>");
            AddLine(ref html, "<script src=\"https://maxcdn.bootstrapcdn.com/bootstrap/3.4.0/js/bootstrap.min.js\"></script>");

            // Align Center
            AddLine(ref html, "<style>.layer { position:absolute; display:table; top:0; left:0; width:100%; height:100% } .layer .layer_inner { display:table-cell; text-align:center; vertical-align:middle } .layer.content { display: inline-block; background:#f00 }</style>");

            // Prevent Mouse Event
            AddLine(ref html, "<script src=\"https://ajax.googleapis.com/ajax/libs/jquery/3.3.1/jquery.min.js\"></script>");
            AddLine(ref html, "<script>jQuery(document).ready(function(){jQuery(function(){jQuery(this).bind(\"contextmenu\",function(event){event.preventDefault();});});});</script>");

            AddLine(ref html, "</head><body scroll=\"no\" onmousedown=\"return false\">");
            AddLine(ref html, raw);
            AddLine(ref html, "</body></html>");
            return html;
        }

        private static void AddLine(ref string html, string line)
        {
            html += line + "\n";
        }
    }
}