var ClipboardPlugin = {
    copyToClipboard: function (text) {
        var actualText = UTF8ToString(text); // Convert pointer to string
        navigator.clipboard.writeText(actualText)
            .then(() => console.log("Copied to clipboard: " + actualText))
            .catch(err => console.error("Clipboard copy failed: ", err));
    }

    
    
    
    
};


mergeInto(LibraryManager.library, ClipboardPlugin);
