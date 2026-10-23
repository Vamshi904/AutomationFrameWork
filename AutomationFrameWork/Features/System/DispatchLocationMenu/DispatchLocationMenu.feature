@Regression
Feature: Dispatch Location menu

Background:
	Given user is on login page
	When user logs in with valid credentials
	Then user should be logged in successfully

@AddDispatchLocation @P1
Scenario: Add a new  Dispatch Location with valid details
	When the user navigates to the System tab
	And Click on the Dispatch Locations  menu
	And click on the Add Dispatch Location button
	And fills in the Dispatch Location information with valid details
	And Click on the Submit Information
	Then the Dispatch Location should appear in the user list
@EditDispatchLocation @P2
Scenario: Edit existing Dispatch Location with valid details
	When the user navigates to the System tab
	And Click on the Dispatch Locations  menu
	And clicks on Dispatch Location Name
	And updates the Edit dispatch location details with valid details
	And clicks on the Submit Information
	Then confirmation message is displayed

@EditDispatchLocation @P3
Scenario: Delete Dispatch Location
	When the user navigates to the System tab
	And Click on the Dispatch Locations  menu
	And Click on the delete icon inList of Dispatch Locations
   

