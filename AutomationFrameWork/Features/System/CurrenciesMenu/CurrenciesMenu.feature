@Regression
Feature: Currencies Menu

Background:
	Given user is on login page
	When user logs in with valid credentials
	Then user should be logged in successfully

@AddCurrency 
Scenario: Add a new Currencies with valid details
	When the user navigates to the System tab
	And Click on the Currencies menu
	And click on the Add Currency button
	And fills in the  Currency Form with valid details
	And Click on the Submit Information in Currency Form page. 
	And Verify the confirmation message is displayed for new added currency page 
@EditCurrency 
	Scenario: Edit existing Currencies with valid details
	When the user navigates to the System tab
	And Click on the Currencies menu
	And click on the Currency Name 
	And Editing existing Currency Form with valid details
	And Click on the Submit Information in Currency Form page. 
	And Verify the confirmation message is displayed

	@SearchFunctionality 
Scenario: Search and view  Currencies with valid details
	When the user navigates to the System tab
	And Click on the Currencies menu
	And Fill the search Name 
	Then print all the details in the Currencies table