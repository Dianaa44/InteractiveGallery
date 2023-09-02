import * as THREE from "/gallery/node_modules/three/src/Three.js";

import { GLTFLoader } from "/gallery/node_modules/three/examples/jsm/loaders/GLTFLoader.js";

export default class Loaders {
    constructor() {
        this.loaders = {};

        this.setLoaders();
    }
    
    setLoaders() {
        this.loaders.cubeTextureLoader = new THREE.CubeTextureLoader();
        this.loaders.gltfLoader = new GLTFLoader();
        this.loaders.textureLoader = new THREE.TextureLoader();
    }
}
