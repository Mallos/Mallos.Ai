namespace Mallos.Insight.Nancy
{
    using global::Nancy;
    using System.Collections.Generic;
    using System.IO;

    static class ResponseHelper
    {
        // Nancy.Responses.Negotiation has support for Content-Types
        // I just find this easier...

        static readonly Dictionary<string, string> contentTypes = new Dictionary<string, string>()
        {
            { "html", "text/html; charset=utf-8" },
            { "txt", "text/plain" },

            // Assets
            { "xml", "application/xml" },
            { "json", "application/json" },
            { "pdf", "application/pdf" },
            { "ttf", "font/ttf" },

            // Images
            { "jpg", "image/jpeg" },
            { "jpeg", "image/jpeg" },
            { "png", "image/png" },
            
            // JavaScript
            { "js", "text/javascript" },
        };

        public static Response FromFile(string filename, string content)
        {
            var filenameSplit = filename.Split('.');
            var filenameExtension = filenameSplit[filenameSplit.Length - 1].ToLower();

            if (!contentTypes.ContainsKey(filenameExtension))
                return Response.NoBody;

            return CreateSimple(contentTypes[filenameExtension], content);
        }

        private static Response CreateSimple(string contentType, string contents)
        {
            return new Response()
            {
                StatusCode = HttpStatusCode.OK,
                ContentType = contentType,
                Contents = stream => AppendStream(ref stream, contents),
                Headers = new Dictionary<string, string>()
            };
        }

        private static void AppendStream(ref Stream stream, string s)
        {
            var writer = new StreamWriter(stream);
            writer.Write(s);
            writer.Flush();
        }
    }
}
