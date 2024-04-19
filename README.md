// To manually create manager accounts follow these steps:
        // Step 1: Go to file Login.razor
        // Step 2: Uncomment line 128: <button class="custom-button" @onclick="CreateAccount">Create Account</button>
        // Step 3: Below Your new manager accounts: (line 188) add new managers like instructed in the examples.
        // Make sure you add the Managers inside the new_managers list bellow line 187.
        // Make sure each manager begins with Guid.NewGuid() for a new ID inside the database (see Example line 180).
        // Make sure each manager ends with managertype to be sure you add a manager account (see Example 180).
        // Make sure to use unique and not already used emails for the accounts to be registerd.
        // Make sure to use correct syntax inside the new managers list! (don't use ; inside lists).
        // Step 4: Do not forget the login information of the managers you are about to register (email, password).
        // Step 5: Save the code.
        // Step 6: Run the application.
        // Step 7: When the loginscreen is loaded you will see a Create Account button, click the button once.
        // Step 8: To confirm the new managers have been registerd, login or check the debug console for: "Regular User ...added." this should appear per registerd manager.
        // Step 9: !! MAKE SURE YOU RE-COMMENT line 128 and save the code to remove the Create Account button. !!

// To run the unit test use this command:
        //  dotnet test Project-C-Test/Project-C-Test.csproj
    
