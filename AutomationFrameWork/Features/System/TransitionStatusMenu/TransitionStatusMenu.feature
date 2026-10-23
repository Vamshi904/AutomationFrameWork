@Regression
Feature: Transition Status menu

Background:
	Given user is on login page
	When user logs in with valid credentials
	Then user should be logged in successfully
   
@AddTransitionStatus @P1
Scenario: Add a new Transition Status  with valid details
	When the user navigates to the System tab
	And Click on the Transition Status menu
	And the user clicks on the Add New Status button
	And fills in the Transition Status Form with valid details
	And Click on the Submit information button in Transition Status Form
	Then Verify the confirmation message is displayed for new Transition Status Page

	@EditTransitionStatus @P2
Scenario: Editing  Transition Status  with valid details
	When the user navigates to the System tab
	And Click on the Transition Status menu
	And Click on the Transition Status link
	And Editing existing Transition Status Form with valid details
	And Click on the Submit information button in Transition Status Form
	Then Verify the confirmation message is displayed for Edit Transition Status Page

	@SearchAndViewFunctionality
	Scenario: Search Functionality and View, printing Transition Status table data
	When the user navigates to the System tab
	And Click on the Agent User menu
	And Enter the name in the search Field.
	Then print all the details in the  Agent Commision table 
