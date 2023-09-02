import * as THREE from "/gallery/node_modules/three/src/Three.js";
import Gallery from "../../../Gallery.js";
import { OctreeHelper } from "/gallery/node_modules/three/examples/jsm/helpers/OctreeHelper.js"

export default class Building {
    constructor() {
        this.gallery = new Gallery();
        this.scene = this.gallery.scene;
        this.resources = this.gallery.resources;
        this.octree = this.gallery.world.octree;

        this.init();
        this.setMaterials();
    }

    init() {
        this.building = this.resources.items.model.gallery.scene;
        console.log(this.building)
        const colider = this.building.getObjectByName("Colider");
        this.octree.fromGraphNode(colider);
        colider.removeFromParent();

        // const helper = new OctreeHelper(this.octree);
        // helper.visible = true;
        // this.scene.add(helper);
    }


    setMaterials() {
        if(artWorks) {
            var idx = 0;
            let textureLoader = new THREE.TextureLoader();
            this.building.children.forEach((child) => {
                if (child.isMesh && child.name.startsWith('artwork')) {
                    let texture = textureLoader.load(artWorks[idx % artWorks.length]);
                    idx ++;
                    texture.flipY = false;
                    texture.center = new THREE.Vector2(0.5, 0.5);
                    texture.rotation = -(Math.PI / 2);
                    child.material = new THREE.MeshBasicMaterial({
                        map: texture,
                    });
                };
            });
            this.scene.add(this.building);
        }
    }
}
