function details(id) {
    let url = 'http://localhost:5223/'
    window.location.href = `${url}Movie/Details/${id}`
};

function watchPremo(src) {
    let myDiv = document.querySelector('.test');
    let video = document.createElement("video");
    video.setAttribute("width", "320");
    video.setAttribute("height", "240");
    video.setAttribute("controls", "controls");
    let srcVideo = document.createElement("source");
    srcVideo.setAttribute("src", `${src}`);
    srcVideo.setAttribute("type", "video/mp4");
    video.appendChild(srcVideo);
    video.style.zIndex = 1000;
    myDiv.appendChild(video);

}