console.log("index");

const fn = () => {
    if(document.getElementById("home")) {
        const page = document.querySelector('.page');

        page.style.cssText = "" +
            "background: url(images/lib.jpg) no-repeat !important;\n" +
            "background-size: cover !important;\n" +
            "color: white !important;\n" +
            "width: 100dvw !important;\n" +
            "height: 100dvh !important;\n" +
            "padding-top: none !important;\n";
    }
}

fn();