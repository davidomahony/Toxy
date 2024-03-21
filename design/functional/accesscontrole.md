# Access Control
While maintaining encryption is the number on priority, have strong access control is also the secondary goal. As we do not want people who do not have sufficent access to detokenize. We also want to log who is detokenizing what.

## Identifying user access
As each request will contain an auth token, we can use this auth token to get the users access information such as username. In the configuration we will also contain a list of users who can detokenize values. Cross checking against this list for know will be enough. But as this grows we may want to consider more of a roll based access.


## Auditing access requests
As a user effectively has to request to detokenize each value we will be able to track what users are detokenizing values. Here we will be able to spot any anomalies or if any unkown users attempt to do so.