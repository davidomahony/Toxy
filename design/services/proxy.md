# Proxy 
The role of the proxy is effectively a translater where it translates tokens to clear values or else replaces sensitive values with tokens. This service will sit in front of API's and should be as transparent as possible.

## Overview of Design
At its very core the proxy will follow a similar pattern to a reverse proxy, where requests will be routed to the proxy and in turn it will forward the requests to the correct API once it has performed the required processing.


