# Encryption Options
Lets go through the various options and the challenges imposed by each one, really there is only two.

## Deterministic
As we will always get the same encrypted value for the same clear text value. We can use multiple iterations of this to give us somenthing more secure and potentially salting.

### Advantage
- Prevents database getting exponentially bigger

### Disadvantage




## Non Deterministic
This is without a doubt the most secure the element of randomness keeps giving us different results from the same value. But this can also result is a great deal of tokens being created for the same value. Which causes the data store to grow exponentially larger as we add more values.

### Advantage 


### Disadvantage
- Causes the database to get exponentially bigger
