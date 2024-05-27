
export default class articleReactionFetchData {
    static toggleLike = async (articleId) => {
        const response = await fetch(`/ArticleReaction/ToggleLike?articleId=${articleId}`)
        return await response.text();
    }
    static toggleDislike = async (articleId) => {
        const response = await fetch(`/ArticleReaction/ToggleDislike?articleId=${articleId}`)
        return await response.text();
    }
    static toggleSupport = async (articleId) => {
        const response = await fetch(`/ArticleReaction/ToggleSupport?articleId=${articleId}`)
        return await response.text();
    }
}
