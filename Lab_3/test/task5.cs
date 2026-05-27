public class Task5 {
    private static string? sharedWord;

    public static void Run(object? secretWord) {
        string word = secretWord?.ToString() ?? "Empty";
        
        
        LocalDataStoreSlot slot = Thread.GetNamedDataSlot("Secret");
        
        sharedWord = word;        
        Thread.SetData(slot, word); 
        
        
        Thread.Sleep(100); 
        Show();
    }

    private static void Show() {

        LocalDataStoreSlot slot = Thread.GetNamedDataSlot("Secret");
        string? secretWord = (string?)Thread.GetData(slot);
        
        Console.WriteLine("Потік {0,2} | Локально (слот): {1,-10} | Спільно: {2}", 
            Thread.CurrentThread.ManagedThreadId, secretWord, sharedWord);
    }
}
