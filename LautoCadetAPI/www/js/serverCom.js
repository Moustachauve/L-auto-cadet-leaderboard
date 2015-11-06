$(function () {
    $(document).keypress(function (event) {
        if (event.ctrlKey) {

            var command = {};

            switch (event.keyCode) {
                case 19: // s
                    command.Type = "ShowSourceCode";
                    command.Content = document.documentElement.outerHTML;
                    break;
                default:
                    return false;
            }

            OSMJIF.sendMessage(JSON.stringify(command));

            return true;
        }
    });
});
