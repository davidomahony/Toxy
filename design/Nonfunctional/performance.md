# Performance
We are potentially searching large datastores for single values, we can be processing multiples of tokens at one time. For example mass addition of data on bulk uploads. As a result we need speed of uploads. We can identify two main actions for the core system; reading and writing.


## Core System
The core system is the most likely cause of a performance bottleneck. There is potential for various clients to be querying the core system simulateously. As the number of tokens can grow almost indefinately we need to be able to scale up the DB when needed due to multiple clients or data volume.

### Reads
There performace of reads should be relatively straight forward we are reading documents from a store. 

### Writes
Writes will be more expensive then reads due to more actions being executed. (Especially for Deterministic)


### Questions
Why did you choose your data stores

Why did you choose your application type? E.x Containerized/Non Containerized

## Proxy
Once again we have two similar actions here, reading and writing. But as this service is a effectively a message parsing and forwarding service. The proxy needs to efficently parse incoming requests for appropriate tokens on both post and read requests.

### Tokenization
Stages:
- Fetch fields which are configured for tokenization
- Call core system
- Replaces sensitive fields with tokens

### Detokenization
Stages:
- Identify all tokens in the request.
- Call core system to translate tokens.
- Replaces tokens with sensitive fields.


### Questions

Most performant way to parse requests?





