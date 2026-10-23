@Regression
Feature: Consignments Menu

Background:
	Given user is on login page
	When user logs in with valid credentials
	Then user should be logged in successfully
   
Scenario Outline: Add a new Transition Point Information with valid details
	When the user navigates to the System tab
	And Click on the Consignments  menu
	And click on the Add new Consignments button
	And fills in the Transition Point form with "<Consignment Name>" "<Short Code>" "<Choose Courier Type>"
	And Click on the submit information button
	Then the new Consignments  should appear in the user list

Examples:
	| Consignment Name | Short Code                      | Choose Courier Type |
	| kamareddy0001    | ADEN ADDE INTERNATIONAL AIRPORT | Air                 |
	| Delhi0102        | Alfa Bright International       | Air                 |
	| Mumbai03         | ADEN ADDE INTERNATIONAL AIRPORT | Air                 |

Scenario: Editing Transition Point Information with valid details
	When the user navigates to the System tab
	And Click on the Consignments  menu
	And Fill the consignment name in the search field
	Then Then print the details in the excel

