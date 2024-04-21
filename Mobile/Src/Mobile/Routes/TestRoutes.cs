namespace Mobile.Routes;

public static class TestRoutes
{
    public static string GetAll = "api/quiz/test";
    public static string GetTestWithQuestionsById(string testId) =>
        $"api/quiz/test/{testId}/questions";

    public static string Create = "api/quiz/test";
    public static string Delete(string testId) =>
        $"api/quiz/test/{testId}";
}