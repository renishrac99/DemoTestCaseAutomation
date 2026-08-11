import os
import subprocess
import google.generativeai as genai
import sys
import re

def get_git_diff():
    # Gets the diff of the latest commit
    try:
        diff = subprocess.check_output(["git", "diff", "HEAD~1", "HEAD"], text=True)
        return diff
    except subprocess.CalledProcessError:
        print("No previous commit found or git error.")
        return ""

def main():
    api_key = os.environ.get("GEMINI_API_KEY")
    if not api_key:
        print("GEMINI_API_KEY is not set.")
        sys.exit(1)

    genai.configure(api_key=api_key)
    
    diff = get_git_diff()
    if not diff or diff.strip() == "":
        print("No changes found in HEAD~1..HEAD.")
        sys.exit(0)

    print("Analyzing diff with Gemini...")
    model = genai.GenerativeModel('gemini-3.6-flash')
    
    prompt = f"""
    You are an expert .NET C# developer and SDET. 
    Review the following git diff and generate or update xUnit/NUnit test cases for the changed code.
    If the changes are in 'DemoTestCaseAutomation.Api', 'Application', or 'Domain', output the full C# test class code that should be added or updated.
    
    Output your response in the following format so it can be parsed:
    
    FILEPATH: DemoTestCaseAutomation.Tests/Services/YourNewTestClass.cs
    ```csharp
    // Your code here
    ```
    
    You can output multiple files by repeating the format. Only output valid C# code. Make sure to use appropriate namespaces based on the DemoTestCaseAutomation.Tests directory.
    
    Here is the git diff:
    {diff}
    """

    try:
        response = model.generate_content(prompt)
        output = response.text
    except Exception as e:
        print(f"Error calling Gemini API: {e}")
        sys.exit(1)
        
    print("Parsing generated tests...")
    
    # Parse the output to extract file paths and code blocks
    file_blocks = re.split(r'FILEPATH:\s*(.+)', output)
    
    if len(file_blocks) <= 1:
         print("No filepaths found in Gemini output. It might have decided no tests were needed or format was wrong.")
         print("Output was:")
         print(output)
         sys.exit(0)
    
    for i in range(1, len(file_blocks), 2):
        filepath = file_blocks[i].strip()
        code_block = file_blocks[i+1]
        
        # Extract just the code inside ```csharp ... ```
        code_match = re.search(r'```(?:csharp|cs)?(.*?)```', code_block, re.DOTALL)
        if code_match:
            code = code_match.group(1).strip()
            
            # Ensure directory exists
            full_path = os.path.join(os.getcwd(), filepath)
            os.makedirs(os.path.dirname(full_path), exist_ok=True)
            
            with open(full_path, "w") as f:
                f.write(code)
            print(f"Wrote generated test to: {filepath}")

    print("Test generation complete.")

if __name__ == "__main__":
    main()
