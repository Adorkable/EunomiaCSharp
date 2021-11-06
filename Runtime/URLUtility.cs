namespace Eunomia
{
    public class UrlUtility
    {
        public enum Protocol
        {
            Http,
            WebSocket
        }

        public static string ProtocolPrefix(Protocol protocol, bool secure)
        {
            switch (protocol)
            {
                case Protocol.Http:
                    return secure
                        ? "https"
                        : "http";

                case Protocol.WebSocket:
                    return secure
                        ? "wss"
                        : "ws";
            }

            return "";
        }

        public static string ComposeUrl(Protocol protocol, bool secure, string host, int port)
        {
            var result = ProtocolPrefix(protocol, secure) + "://" + host;

            // Make this removal optional
            if (port != 80)
            {
                result += ":" + port;
            }

            return result;
        }
    }
}