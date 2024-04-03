# Access Control
While maintaining encryption is the number on priority, have strong access control is also the secondary goal. As we do not want people who do not have sufficent access to detokenize. We also want to log who is detokenizing what.

## Identifying user access
As each request will contain an auth token, we can use this auth token to get the users access information such as username. In the configuration we will also contain a list of users who can detokenize values. Cross checking against this list for know will be enough. But as this grows we may want to consider more of a roll based access.


## Auditing access requests
As a user effectively has to request to detokenize each value we will be able to track what users are detokenizing values. Here we will be able to spot any anomalies or if any unkown users attempt to do so.


## Monitoring 
As we have such control over when people request data we can really see who is accessing what, we can use some sort of detection to identify anomalies. Aalso its a different system making multiple layers of access control


## Potential Teams
One are we should consider here, is that there may be various levels of controls, these should be based by the person who creates the control. Although this does mean we need to be able to modify access levels