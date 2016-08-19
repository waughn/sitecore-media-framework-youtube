;
var youtubeListener = new PlayerEventsListener();

youtubeListener.getEventType = function (event) {
    switch (event.target.getPlayerState()) {
        case YT.PlayerState.ENDED:
            return this.eventTypes.PlaybackCompleted;
        case YT.PlayerState.PLAYING:
            return this.eventTypes.PlaybackStarted;
        case YT.PlayerState.PAUSED:
            return this.eventTypes.PlaybackChanged;
        case YT.PlayerState.BUFFERING:
            return undefined;
        case YT.PlayerState.CUED:
            return undefined;
        default:
            return undefined;
    }
};

youtubeListener.getMediaId = function (event) {
    return event.target.getVideoData().video_id;
};

youtubeListener.getPosition = function (event) {
    return event.target.getCurrentTime();
};

youtubeListener.getDuration = function (event) {
    return Math.round(event.target.getDuration());
};

youtubeListener.getContainer = function (event) {
    return this.jQuerySMF('#' + event.target.getIframe().id).closest('.mf-player-container');
};

youtubeListener.getAdditionalParameters = function (event) {
    return {
        mediaId: event.target.getVideoData().video_id,
        mediaName: event.target.getVideoData().title,
        mediaLength: event.target.getDuration()
    };
};

youtubeListener.onPlayerReady = function (event) {
};

youtubeListener.onPlayerStateChange = function (event) {
    youtubeListener.onMediaEvent(event);
};

youtubeListener.onPlayerError = function (event) {
    youtubeListener.onMediaEvent(event, this.eventTypes.PlaybackError);
};

function onYouTubeIframeAPIReady() {
    var iframe = this.jQuerySMF('iframe[id^="ytplayer"]');

    if (iframe) {
        var player = new YT.Player(iframe.attr('id'), {
            events: {
                'onReady': youtubeListener.onPlayerReady,
                'onStateChange': youtubeListener.onPlayerStateChange,
                'onError': youtubeListener.onPlayerError
            }
        });
    }
};
