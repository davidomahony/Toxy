# Purging Tokens

## Options for purging
We have three options for purging these, but there is no real way of getting around it. If we cant remove tokens over time it will make the application slower.

### Jobs runing a cadence
A fixed job which runs on a certain cadence it will run on each data store and gets the count of each token when it encounters one potentially in some sort of cache. Once the jobs have run we should have identified tokens which are no longer in use.

### Demotion after time period
A fixed time period which identifies tokens which have not been read in x period of time. After so the cooling off process starts. This is likely the easiest option, but really may not be good enough. An easy solution but has the potential to lose data.

### Notification of deleted tokens
This one needs a little bit more work, as we will need to keep count of how many times a token has been created to match it up with the number of times its been deleted. Although this could be tricky as in a datastore the tenant may be duplicating without us known. This option adds a lot of complexity and requires lots of implementation on the tenants side. Which is not ideal.

## Purging Process
We can have a hard delete where once these tokens are identified thery are gone. Could cause issues if tokens are incorrectly identified as deleted. Therefore a cooling down process may be better, where we archive tokens for a certain length of time in a different datastore which literally has no access. These must be deleted, I dont want these hanging around.



## Problems (This should probably be in a different file)
We need a way to clean up tokens

- So option one is we make a hot or cold path, but this gets tricky, when do we look in the cold path?
- What about a mechanism to see if tokens should be purged, so we need to see when reads occur followed.

- Maybe an event hook to archive tokens, but deleting tokens is kind of risky straight away, perhaps a cool down process before deletion is better.

- Perhaps rules around how a token transitions from a hot path to a cold, which state if a token has not been used in x amount of time it is sent to this cold path which is eventually deleted. The best way to gaurd PII is to delete it

- This idea of a yearly purge would work as well its just very intrusive.
    - The idea of a daily or monthly job which runs to see which tokens exist in the customer database this basically identifies tokens which exist in my vaul but not in the customers data stores. In turn these are then invalidated in the vault for a period of time before being deleted. We need to audit all these.
- A web hook idea of an on delete event is nice also, but intrusive

