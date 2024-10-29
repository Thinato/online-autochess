using common;

namespace server;

public abstract class RequestHandler {

    public abstract string Handle(RequestContext context);

}