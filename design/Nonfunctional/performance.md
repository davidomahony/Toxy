# Performance
We are potentially searching large datastores for single values, we can be processing multiples of tokens at one time. For example mass addition of data on bulk uploads. As a result we need speed of uploads. We can identify two main actions for the core system; reading and writing.


## Core System

### Reads


### Writes


## Proxy
Once again we have two similar actions here, reading and writing.

### Tokenization
Writing involves parsing a request for known fields which will be tokenized and in turn calling downstrweam services for these tokens. So we need to quickly parse this.

## Detokenization
Main action here is parsing the request and identifying tokens not 100% sure potentially some sort of regex.

