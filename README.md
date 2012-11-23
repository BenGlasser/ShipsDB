CS 386/586 Fall 2012  Exercise 5 Database Design using ERDs
============================================================
 
Due: Thursday, November 28, 2012 at the beginning of the class period
 
Choose one of the three databases from our book – either the PC database, the Ships database, or the Movie database.

Choose an implementation of embedded SQL where a programming language is used with SQL.  You are welcome to choose any of the languages or DBMSs that were used by the graduate student teams.

Implement the following application:

a.       Allow the user to choose a table (in the DB that you’ve chosen).  Allow the user to insert two rows at one time into this table.  If you use a form, then you should have two fields for each attribute of the table.  If you ask for input from the user, you should get the input for both tuples before you do the insert operation.

b.      Allow the user to choose a table (in the DB that you’ve chosen).  Allow the user to enter a single value for one of the non-key attributes of the table.  Show the result of a query that includes all of the rows from the table where the attribute value of the row matches the value that the user entered.


Please turn in: your program, your database schema.
For part a., turn in a screenshot of the user input plus a copy of the rows in the database table before and after this operation is issued.
for part b., turn in a copy of the rows in the database table before this operation is issued plus a screenshot of the output shown to the user.
Show the results of part b. twice; once when the query answer is empty and once when the query answer (on a different attribute) has more than one row.
 
Please turn in a very short writeup of your implementation describing what components you used, how you installed your software, which graduate student assignment you based your implementation on (if any), and a description of whether you encountered any problems or difficulties.  Finally, describe whether you learned anything new by implementing this application.