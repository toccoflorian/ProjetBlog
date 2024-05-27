import articleReactionFetchData from "../services/ArticleReactionService.js";

const manageResponse = (response) => {
    document.getElementById('articleReactionBarContainer').innerHTML = response;
    createArticleReactionEventListeners();
}

const createArticleReactionEventListeners = () => {

    document.getElementById('likeContainer').addEventListener('click', async e => {
        manageResponse(await articleReactionFetchData
            .toggleLike(e.currentTarget
                .getAttribute('data-article-id')));});

    document.getElementById('dislikeContainer').addEventListener('click', async e => {
        manageResponse(await articleReactionFetchData
            .toggleDislike(e.currentTarget
                .getAttribute('data-article-id')));});

    document.getElementById('supportContainer').addEventListener('click', async e => {
        manageResponse(await articleReactionFetchData
            .toggleSupport(e.currentTarget
                .getAttribute('data-article-id')));});
}

createArticleReactionEventListeners();