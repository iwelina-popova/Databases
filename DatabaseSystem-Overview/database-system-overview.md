# Database Systems Overview Homework

## What database models do you know?
* Hierarchical model
* Network model
* Relational model
* Rentity-relational model
* Document model
* Star schema
* Object-oriented models

## Which are the main functions performed by a Relational Database Management System (RDBMS)?
* Creating / altering / deleting tables and relationships between them (database schema)
* Adding / changing / deleting/ searching and retrieving of data stored in the tables
* Support for the SQL language
* Transaction management (optional)

## Define what is "table" in database terms.
* A table is a collection of related data held in a structured format within a database. It consists of fields (columns), and rows.
* In relational databases and flat file databases:
   *  Set of data elements (values) using a model of vertical columns (identifiable by name) and horizontal rows
   *  The cell being the unit where a row and column intersect
   *  A table has a specified number of columns, but can have any number of rows
   *  The columns subset which uniquely identifies a row is called the primary key
   
## Explain the difference between a primary and a foreign key.
* Primary key:
  * A unique key
  * A set of zero, one, or more attributes
  * The value(s) of these attributes are required to be unique for each tuple (row) in a relation
* Foreign key:
  * A field (or collection of fields) in one table that uniquely identifies a row of another table
  * The foreign key is defined in a second table, but it refers to the primary key in the first table
  
## Explain the different kinds of relationships between tables in relational databases.
### There are three types of relationships:
  * One-to-one: Both tables can have only one record on either side of the relationship. Each primary key value relates to only one (or no) record in the related table. Most one-to-one relationships are forced by business rules and don't flow naturally from the data. In the absence of such a rule, you can usually combine both tables into one table without breaking any normalization rules.
  * One-to-many: The primary key table contains only one record that relates to none, one, or many records in the related table.
  * Many-to-many: Each record in both tables can relate to any number of records (or no records) in the other table. For instance. Many-to-many relationships require a third table, known as an associate or linking table, because relational systems can't directly accommodate the relationship.
  
## When is a certain database schema normalized?
* Database normalization (or normalisation) is the process of organizing the columns (attributes) and tables (relations) of a relational database to minimize data redundancy
* Normalization involves decomposing a table into less redundant (and smaller) tables without losing information

## What are database integrity constraints and when are they used?
Integrity constraints provide a mechanism for ensuring that data conforms to guidelines specified by the database administrator:
* UNIQUE constraints to ensure that a given column is unique
* NOT NULL constraints to ensure that no null values are allowed
* PRIMARY KEY is a column of the table that uniquely identifies its rows (usually its is a number)
* FOREIGN KEY constraints to ensure that two keys share a primary key to foreign key relationship

## Point out the pros and cons of using indexes in a database.
The indexing increases the disk space usage and reduces the performance of adding, deleting, and updating, but in most cases the benefit of indices for data retrieval greatly exceeds the disadvantages.
* *__Pros:__*
  * Fast searching
  * Fast sorting
* *__Cons:__*
  * Slow insert
  * Slow update
  
## What's the main purpose of the SQL language?
SQL (Structured Query Language) is a special-purpose programming language designed for managing data held in a relational database management system (RDBMS), or for stream processing in a relational data stream management system (RDSMS).

## What are transactions used for?
A transaction is a unit of work that is performed against a database. Transactions are units or sequences of work accomplished in a logical order, whether in a manual fashion by a user or automatically by some sort of a database program.
* *__Example:__*
 ```
Request: transfer 900$ from Account 9001 to 9002

start transaction
select balance from Account where Account_Number='9001';
select balance from Account where Account_Number='9002';
update Account set balance=balance-900 here Account_Number='9001' ;
update Account set balance=balance+900 here Account_Number='9002' ;
commit; //if all sql queries succed
rollback; //if any of Sql queries failed or error
```

## What is a NoSQL database?
A NoSQL (originally referring to "non SQL" or "non relational") database provides a mechanism for storage and retrieval of data that is modeled in means other than the tabular relations used in relational databases. Such databases have existed since the late 1960s, but did not obtain the "NoSQL" moniker until a surge of popularity in the early twenty-first century, triggered by the needs of Web 2.0 companies such as Facebook, Google and Amazon.com.

NoSQL databases are increasingly used in big data and real-time web applications. NoSQL systems are also sometimes called "Not only SQL" to emphasize that they may support SQL-like query languages

## Explain the classical non-relational data models.
* Key-value (KV) - stores use the associative array (also known as a map or dictionary) as their fundamental data model. In this model, data is represented as a collection of key-value pairs, such that each possible key appears at most once in the collection.
* Document store - While each document-oriented database implementation differs on the details of this definition, in general, they all assume that documents encapsulate and encode data (or information) in some standard formats or encodings.
* Graph - This kind of database is designed for data whose relations are well represented as a graph (elements interconnected with an undetermined number of relations between them). The kind of data could be social relations, public transport links, road maps or network topologies, for example.

## Give few examples of NoSQL databases and their pros and cons
* *__Pros:__*
  * It's Open Source - Open source products provide developers with some great benefits, notably their no-cost status. Other benefits: open-source software tends to be more reliable, secure, and faster to deploy than proprietary alternatives.
  * Elastic Scaling - NoSQL replaces the old 'scale up' mantra of database managers with a new one: 'scale out.' Instead of adding bigger servers to handle more data load, a NoSQL database allows a company to distribute the load across multiple hosts as the load increases.
  * Different NoSQL DBs for Different Projects - MongoDB and Redis are good choices for storing frequently-written, rarely read statistical data, such as a web hit counter. Hadoop, a freeform, distributed DB does a good job of storing Big Data such as weather stats or business analytics work. Memcache, a transient DB, excels at storing web sessions, locks, and short-term stats). Cassandra and Riak (automatically clustered, redundant stores) perform well in environments with high availability apps, when maximum uptime is vital.
  * NoSQL’s Impressive Implementations - Companies such as Amazon, the BBC, Facebook and Google rely on NoSQL DBs.
  * NoSQL Flies High in the Cloud - NoSQL and the cloud are a natural fit. Today’s servers are inexpensive and can easily be scaled up on demand using a service such as Amazon EC2. Like all cloud technology, EC2 is based on virtualization. The weak link of virtualization is I/O performance, with memory and CPU being the strong links. NoSQL databases mostly use memory over disk as the primary write location – thereby preventing inconsistent I/O performance. And since NoSQL datastores typically leverage horizontal scalability via partitioning, they are able to take advantage of the cloud’s elastic provisioning.
* *__Cons:__* 
  * Open Source can Mean Spotty Support for Enterprises
  * Not Mature Enough for Enterprises
  * Business Intelligence Limitations
  * Lack of Expertise
  * Compatibility Issues





