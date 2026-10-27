Feature: Search

@Loan#TabSearchUsingPartialLoanNumber 
@ignore
Scenario: Search loan using partial loan number (1245) and an exact loan number (309991245) in the loan# tab of Search Menu
	Given I navigate to the application url