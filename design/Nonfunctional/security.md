# Security 
Here we need to call out security measures taken for two areas, number one the system as a whole. Number two the Vault solution what levels of security are used to allow a client to place trust in us with there most sensitive data.


I am just putting this here to ensure our system security alligns to the appropriate values
- Confidentiality
- Intengrity
- Availability
- AUthenticity
- Non reputability
- Govern
- Protect
- Detect
- Respond

Notable Mentions
Least privilege, Encryption, Authentiction Enhancment, Monitoring Data




## System security as a whole

### Example Request Security

## Vault solution security


When a value is recieved for tokenization it is instantly encryted, these values are then stored in the in a data store. WHats the seller here? I need way more then this. Is there any way I can say this is encryted three times. Lets salt this aswell. Perhaps the proxy could perform one final step also?

1. Encryption in transit using TLS between cluster communication
2. Encryption at rest
    - WiredTiger Engine:
    - Atlas
3. Encryption in use
    - This is the actual encryption we will do (Need a deep dive on this)

token --> Encrypted Value
How is this encrypted?
What data store is this encrypted in?
