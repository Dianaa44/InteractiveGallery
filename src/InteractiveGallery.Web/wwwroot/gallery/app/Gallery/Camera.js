import * as THREE from "/gallery/node_modules/three/src/Three.js";
import Gallery from "./Gallery.js";

export default class Camera {
    constructor() {
        this.gallery = new Gallery();
        this.sizes = this.gallery.sizes;
        this.scene = this.gallery.scene;
        this.canvas = this.gallery.canvas;
        this.params = {
            fov: 75, // field of view
            aspect: this.sizes.aspect,
            near: 0.01,
            far: 1000,
        };

        this.setPerspectiveCamera();
    }

    setPerspectiveCamera() {
        this.perspectiveCamera = new THREE.PerspectiveCamera(
            this.params.fov,
            this.params.aspect,
            this.params.near,
            this.params.far
        );

        this.perspectiveCamera.position.set(0, 1.7, 0);

        this.scene.add(this.perspectiveCamera);
    }

    onResize() {
        this.perspectiveCamera.aspect = this.sizes.aspect;
        this.perspectiveCamera.updateProjectionMatrix();
    }

    update() {
        
    }
}
