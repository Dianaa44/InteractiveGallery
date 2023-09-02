import * as THREE from "/gallery/node_modules/three/src/Three.js";
import Gallery from "./Gallery.js";

export default class Renderer {
    constructor() {
        this.gallery = new Gallery();
        this.sizes = this.gallery.sizes;
        this.scene = this.gallery.scene;
        this.canvas = this.gallery.canvas;
        this.camera = this.gallery.camera;

        this.setRenderer();
    }

    setRenderer() {
        this.renderer = new THREE.WebGLRenderer({
            canvas: this.canvas,
            antialias: true,
        });
        this.renderer.outputEncoding = THREE.sRGBEncoding;
        this.renderer.toneMapping = THREE.CineonToneMapping;
        this.renderer.toneMappingExposure = 1.75;
        this.renderer.setSize(this.sizes.width, this.sizes.height);
        this.renderer.setPixelRatio(this.sizes.pixelRatio);
    }

    onResize() {
        this.renderer.setSize(this.sizes.width, this.sizes.height);
        this.renderer.setPixelRatio(this.sizes.pixelRatio);
    }

    update() {
        this.renderer.render(this.scene, this.camera.perspectiveCamera);
    }
}
