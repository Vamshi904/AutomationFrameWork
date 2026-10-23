@Regression
Feature: Customersmenu

Background:
	Given user is on login page
	When user logs in with valid credentials
	Then user should be logged in successfully
   
   @ACustomer @P1
Scenario: Add a new  Customer with valid details
	When the user navigates to the  Customer tab
	And the user clicks on the Add  Customer button
	And fills in the  Customer Form with valid details
	And Click on the Submit information button in  Customer Form
	Then Verify the confirmation message is displayed for new  Customer Form

	   @ACustomer @P1
Scenario: Edit Customer with valid details
	When the user navigates to the  Customer tab
	And the user clicks on the customer Name link 
	And Edit in the  Customer Form with valid details
	And Click on the Submit information button in  Customer Form
	Then Verify the confirmation message is displayed for Edit  Customer Form