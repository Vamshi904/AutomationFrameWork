@Regression
Feature: User Management - Add User

Background:
	Given user is on login page
	When user logs in with valid credentials
	Then user should be logged in successfully
   
@AddUser
Scenario: Add a new user with valid details
	When the user navigates to the System tab
	And clicks on Add User
	And fills in the user information form with valid details
	And saves the new user
	Then the new user should appear in the user list
   
@EditUser
Scenario: Edit existing user with valid details
	When the user navigates to the System tab
	And the user clicks on a username
	And the user edits the existing information with valid details
	And the user clicks on the Update button
	Then the confirmation message should appear in the user list

@DeleteUser
Scenario: Verify Delete the User from the User table
	When the user navigates to the System tab
	And the user enters the name in the Search field
	And Clicks on the delete icon
	Then the confirmation alert should be displayed


@UserTable
Scenario: View Couriers table data
	When the user navigates to the System tab
	Then print all the details in the  User table
