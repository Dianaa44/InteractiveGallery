import * as THREE from "/gallery/node_modules/three/src/Three.js";
import { EventEmitter } from "/gallery/node_modules/events/events.js";
import Gallery from "../Gallery.js";

import { Octree } from "/gallery/node_modules/three/examples/jsm/math/Octree.js";

import Player from "./Player/Player.js";

import Model from "./Model/Model.js";

export default class World extends EventEmitter {
    constructor() {
        super();
        this.gallery = new Gallery();
        this.resources = this.gallery.resources;

        this.octree = new Octree();

        this.localStorage = this.gallery.localStorage;
        this.state = this.localStorage.state;

        this.resources.determineLoad(this.state.location);

        this.player = null;

        this.resources.on("ready", () => {
            if (this.player === null) {
                this.player = new Player();
            }
            this.setWorld();
        });
    }

    setWorld() {
        this.model = new Model();
    }

    update() {
        if (this.player) this.player.update();
    }
}
