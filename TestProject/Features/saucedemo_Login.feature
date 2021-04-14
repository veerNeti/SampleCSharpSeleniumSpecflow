Feature: saucedemo_Login
	Accepted usernames are:
		standard_user
		locked_out_user
		problem_user
		performance_glitch_user
	Password for all users:
		secret_sauce

@Login
Scenario Outline: Login to saucedemo
	Given I navigate to demo URL "https://www.saucedemo.com/"
	And Enter demo User "<user>"
	And Enter demo password "<password>"
	When I click on Login button
	Then User "<flg>" be able to navigate to "<validation>" page

	Examples:
		| user                    | password     | flg       | validation |
		| standard_user           | secret_sauce | should    | inventory  |
		| locked_out_user         | secret_sauce | should    | inventory  |
		| locked_out_user         | secret_sauce | shouldNOT | inventory  |
		| problem_user            | secret_sauce | should    | inventory  |
		| performance_glitch_user | secret_sauce | should    | inventory  |