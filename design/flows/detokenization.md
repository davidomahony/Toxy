# Detokenization Flow
Here we will outline the process used to fetch clear values for preivously generated tokens.

## Overview
A token value is recieved such as "<tokensecret>" this value is then converted to its clear value "Dave". There is no need to seperate deterministic and non deterministic approaches here as they both follow an identical model.

### Deterministic steps:
1. Token recieved.
2. User access validated.
3(A). 
    - Invalid Access
    - Return the token value just as recieved
3(B).
    - User Contains Valid Access
    - Value is detokenized and clear value returned
4. Auditing of request.


##TODO DIAGRAM

#### Note
- Potentially down the line this may need a more granualar control. Ex Compliance team


#### Edge Cases



