namespace Eunomia {
    public class URLUtility {
        public enum Protocol {
            HTTP,
            WebSocket
        }
        public static string ProtocolPrefix(Protocol protocol, bool secure) {
            switch (protocol) {
                case Protocol.HTTP:
                    if (secure) {
                        return "https";
                    }
                    return "http";

                case Protocol.WebSocket:
                    if (secure) {
                        return "wss";
                    }
                    return "ws";
            }

            return "";
        }
        public static string ComposeURL(Protocol protocol, bool secure, string host, int port) {
            string result = URLUtility.ProtocolPrefix(protocol, secure) + "://" + host;

            // Make this removal optional
            if (port != 80) {
                result += ":" + port;
            }

            return result;
        }
    }
}