using System.Collections.Generic;

public class QuestionLog {
    private List<Question> log;
    private int maxCount;
    
    public QuestionLog(int count) {
        log = new List<Question>();
        maxCount = count;
    }

    public List<Question> Get() => log;

    public void Clear() => log.Clear();

    public bool Contains(Question question) => log.Contains(question);

    public void Add(Question question) {
        log.Add(question);
        if(log.Count > maxCount) log.RemoveAt(0);
    }
}