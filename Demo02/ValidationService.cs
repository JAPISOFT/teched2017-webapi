namespace Demo02
{
    public static class ValidationService
    {
	    public static bool ValidateTitle(string title, out string errorMessage)
	    {
		    errorMessage = null;

		    if (title != null && title.StartsWith("C"))
		    {
			    errorMessage = "Title cannot starts with C";
			    return false;
		    }

		    return true;
	    }
    }
}
