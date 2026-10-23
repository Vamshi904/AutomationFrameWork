@Regression
Feature: Agent Commission Menu


Background:
	Given user is on login page
	When user logs in with valid credentials
	Then user should be logged in successfully
   @AddAgentCommissionForm
Scenario: Add a neew Agent Commissions with valid details
	When the user navigates to the System tab
	And Click on the Agent Commissions menu
	And the user clicks on the Add Agent Commission button
	And fills in the Agent commission Form with valid details
	And Click on the Submit information button in Agent Commission Form
	Then Verify the confirmation message is displayed for new added agent commission
@EditAgentCommissionForm
Scenario: Editing existing Agent Commissions  with valid details
	When the user navigates to the System tab
	And Click on the Agent Commissions menu
	And the user clicks on the Add Agent Name
	And Editing the Agent commission Form
	And Click on the Submit information button in Agent Commission Form
	Then Verify the confirmation message is displayed for Editing agent commission

	 @DeleteAgent  @P3
   Scenario: Delete Agent Commision  
   When the user navigates to the System tab
   And Click on the Agent User menu
   And Click on the delete icon in List of Agent Commissions 
  Then Verify the confirmation message is displayed for Delete agent Commission 

  Scenario: Search Functionality and View, printing Agent Commision table data
	When the user navigates to the System tab
	And Click on the Agent User menu
	And Enter the name in the search Field.
	Then print all the details in the  Agent Commision table 







