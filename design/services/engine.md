# Engine Design
For the sake of getting this up off the ground I am going to effectively put the tokenization and configuration in the same service for now. Ideally this would be seperated as really setting the configuration should be handled by a different service. But lets monolith this bad boy to get started.

## The role of the Engine
The engine will take care of two aspects: (for now) its primary role is tokenizing/detokenizing values. This will always remain as the engines main functionality. Its secondary role will involve enabling configuration settings for tenants, lets try and keep these in different DB's to allow a clean split later.


### Tokenization Request
The engine will receive requests from other service requesting a value be tokenized using a certain technique, this needs to be able to do several tokens in one go while being able to identify which token in the response.


#### General DTO's
Tokenization Request
    // A collection with objects detailing each tokenization request
    TokenizationInformation
        // String containing value to be tokenized
        Value
        // String indicatingt technique Used
        Method
        // Id just to identify the token request number likely just ints from 1 - X
        Id


Tokenization Response
    // A collection with objects detailing each tokenization request
    TokenizationInformation
        // String containing new token
        Value
        // Id just to identify the token request number
        Id

When performing this request we need to check if a token exists already and if so lets give them that. Lets avoid adding duplicate tokens to collection.


