namespace TimeTracker.BLL.Exceptions;

public static class ErrorMessages
{
    public const string ModelIsNull = "Model is null";
    public const string CardNotFound = "Card not found";
    public const string UserNotFound = "User not found";
    public const string CardUidRequired = "CardUid is required";
    
    public const string NegativeOffset = "Offset cannot be negative";
    public const string TakeLessThatOne = "Take must be greater than 0";
}