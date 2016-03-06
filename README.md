#cSharpHairSalon

####This is a website for Hair Salon application written in cSharp
####By: Neil Larion

## Description

###This is a project that allows the user to store hair stylist information for clients at their hair salon. It also meets the following criteria:
* Each client only belongs to a single stylist.
* Appropriate view files with RESTful reoutes.
* Has CRUD functionality for each class.

This project was developed during a "Code Review" session at Epicodus, which is a solo project designed to test our knowledge of the materials learned over the previous week's study. It's an honor to be apart of a coding community where people are genuinely interested in what they're doing.

## Setup/Installation Requirements
- Clone this repository.
- Use the .sql in the root directory to make the databases. Or follow these commands in SQLCMD/SQL Server to create hair_salon and hair_salon_test:
  * CREATE DATABASE hair_salon;
  - GO
  - CREATE TABLE stylists (id INT IDENTITY(1,1), name VARCHAR(255));
  - CREATE TABLE clients (id INT IDENTITY(1,1), name VARCHAR(255), appointment DATE, phone VARCHAR(255), email VARCHAR(255), stylist_id INT);
  - GO
- Install Nancy the web viewer
- Build the project using "dnu restore".
- Run the project by calling "dnx kestrel"
- I suggest chrome or chromium to run the site.

## Support and contact details
* http://www.neillarion.com
* neil.larion@gmail.com
* [@nlarion](https://twitter.com/nlarion)

## Technologies Used
* SQL
* CSS
* HTML
* C#
* Bootstrap
* Nancy
* Razor web engine

### License

This work is licensed under a [Creative Commons Attribution 4.0 International License.](http://creativecommons.org/licenses/by/4.0/) 2016
