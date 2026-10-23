@Regression
Feature: Agentmenu
 
Background:
	Given user is on login page
	When user logs in with valid credentials
	Then user should be logged in successfully
 
@AddAgent @P1
Scenario: Add a new Agent with valid details
	When the user navigates to the System tab
	And Click on the Agent User menu
	And the user clicks on the Add Agent button
	And fills in the Agent Form with valid details
	And Click on the Submit information button in Agent Form
	Then Verify the confirmation message is displayed for new added agent
 
@EditAgent @P2
Scenario: Editing existing Agent with valid details
	When the user navigates to the System tab
	And Click on the Agent User menu
	And the user clicks on the Name link
	And Edit in the Agent Form with valid details
	And Click on the Submit information button in Agent Form
	Then Verify the confirmation message is displayed for Edit agent
@DeleteAgent @P3
Scenario: Delete Agent
	When the user navigates to the System tab
	And Click on the Agent User menu
	And Click on the delete icon in Agent page
	Then Verify the confirmation message is displayed for Delete agent
 
Scenario: View Agent table data
	When the user navigates to the System tab
	And Click on the Agent User menu
	Then print all the details in the  Agent table