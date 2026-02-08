document.addEventListener("DOMContentLoaded", () => {

    const domain = "meet.jit.si";

    if (!ROOM_NAME) {
        console.error("ROOM_NAME vacío");
        return;
    }

    const options = {
        roomName: ROOM_NAME,
        parentNode: document.querySelector('#jitsi-meet'),
        userInfo: {
            displayName: USER_NAME
        },
        configOverwrite: {
            startWithAudioMuted: true,
            startWithVideoMuted: true,
            prejoinPageEnabled: false
        },
        interfaceConfigOverwrite: {
            SHOW_JITSI_WATERMARK: false,
            SHOW_WATERMARK_FOR_GUESTS: false,
            DEFAULT_BACKGROUND: '#000000',
            TOOLBAR_ALWAYS_VISIBLE: true,
            TOOLBAR_BUTTONS: ES_DOCENTE
                ? ['microphone', 'camera', 'desktop', 'chat', 'hangup']
                : ['microphone', 'chat', 'hangup']
        }
    };

    const api = new JitsiMeetExternalAPI(domain, options);

    api.addEventListener('toolbarButtonClicked', (event) => {
        if (event.key === 'hangup') {
            api.dispose();
            window.location.href = VOLVER_URL;
        }
    });

});
