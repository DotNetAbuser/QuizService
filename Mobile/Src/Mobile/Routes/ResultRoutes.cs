namespace Mobile.Routes;

public static class ResultRoutes
{
    public static string Create = "api/quiz/result";

    public static string GetPaginatedUsersTestResults(
        int pageNumber, int pageSize, string searchTerm, string sortOrder) =>
        $"api/quiz/result" +
            $"?pageNumber={pageNumber}" +
            $"&pageSize={pageSize}" +
            $"&searchTerm={searchTerm}" +
            $"&sortOrder={sortOrder}";

    public static string GetPaginatedUserTestResultsByUserId(string userId,
        int pageNumber, int pageSize, string searchTerm, string sortOrder) =>
        $"api/quiz/result/{userId}" +
            $"?pageNumber={pageNumber}" +
            $"&pageSize={pageSize}" +
            $"&searchTerm={searchTerm}" +
            $"&sortOrder={sortOrder}";
}