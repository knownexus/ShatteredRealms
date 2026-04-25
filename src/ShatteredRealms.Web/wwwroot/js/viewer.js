window.SRViewer = {
    createBlobUrl: function (base64, contentType) {
        const binary = atob(base64);
        const bytes = new Uint8Array(binary.length);
        for (let i = 0; i < binary.length; i++) bytes[i] = binary.charCodeAt(i);
        const blob = new Blob([bytes], { type: contentType });
        return URL.createObjectURL(blob);
    },
    revokeBlobUrl: function (url) {
        if (url) URL.revokeObjectURL(url);
    }
};
