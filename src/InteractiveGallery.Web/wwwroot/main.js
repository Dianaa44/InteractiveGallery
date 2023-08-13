import * as THREE from 'three';
import { OrbitControls } from 'three/addons/controls/OrbitControls.js';
import { GLTFLoader } from 'three/addons/loaders/GLTFLoader.js';

//===================================================== canvas
var renderer = new THREE.WebGLRenderer({ alpha: true, antialiase: true });
renderer.setSize(window.innerWidth, window.innerHeight);
document.body.appendChild(renderer.domElement);
const loader = new GLTFLoader();
var camera = new THREE.PerspectiveCamera(75, window.innerWidth / window.innerHeight, 0.1, 1000);
const controls = new OrbitControls(camera, renderer.domElement);
//controls.listenToKeyEvents(window);
controls.enableDamping = true; 
controls.dampingFactor = 0.1;
controls.screenSpacePanning = false;

controls.minDistance = 1;
controls.maxDistance = 10;
//controls.target.set(0, 1.5, 0);
controls.maxPolarAngle = Math.PI / 2;

//===================================================== scene
var scene = new THREE.Scene();
const raycaster = new THREE.Raycaster();
//===================================================== camera
//camera.position.z = 5;
//camera.position.y = 1.5;
// Set initial camera position
camera.position.set(0, 1.5, 3);
camera.lookAt(0, 1.5, 0);
//===================================================== lights
//var light = new THREE.DirectionalLight(0xefefff, 3);
//light.position.set(1, 1, 1).normalize();
//scene.add(light);
//var light = new THREE.DirectionalLight(0xffefef, 3);
//light.position.set(-1, -1, -1).normalize();
//scene.add(light);

//===================================================== resize

//window.addEventListener("resize", function () {
//    let width = window.innerWidth;
//    let height = window.innerHeight;
//    renderer.setSize(width, height);
//    camera.aspect = width / height;
//    camera.updateProjectionMatrix();
//});

//var moveSpeed = 0.1;
//var rotateSpeed = 0.02;

//window.addEventListener('keydown', function (event) {
//    switch (event.key) {
//        case 'ArrowUp':
//            camera.translateZ(-moveSpeed);
//            break;
//        case 'ArrowDown':
//            camera.translateZ(moveSpeed);
//            break;
//        case 'ArrowLeft':
//            camera.rotation.y += rotateSpeed;
//            break;
//        case 'ArrowRight':
//            camera.rotation.y -= rotateSpeed;
//            break;
//    }
//});



var speed = 0.1;
var rotateSpeed = 0.02;
var moveForward = false;
var moveBackward = false;
var rotateLeft = false;
var rotateRight = false;



document.addEventListener('keydown', function (event) {
    switch (event.key) {
        case 'ArrowUp':
            moveForward = true;
            break;
        case 'ArrowDown':
            moveBackward = true;
            break;
        case 'ArrowLeft': 
            rotateLeft = true;
            break;
        case 'ArrowRight':
            rotateRight = true;
            break;
    }
}, false);

document.addEventListener('keyup', function (event) {
    switch (event.key) {
        case 'ArrowUp':
            moveForward = false;
            break;
        case 'ArrowDown':
            moveBackward = false;
            break;
        case 'ArrowLeft':
            rotateLeft = false;
            break;
        case 'ArrowRight':
            rotateRight = false;
            break;
    }
}, false);





function update() {
   // debugger;
    var intersects = raycaster.intersectObjects(scene.children, true);
    if (intersects.length == 0) {
        if (moveForward) camera.translateZ(-speed);
        if (moveBackward) camera.translateZ(speed);
        if (rotateLeft) camera.rotation.y += rotateSpeed;
        if (rotateRight) camera.rotation.y -= rotateSpeed;
    }
    
}


//===================================================== model
//var loader = new THREE.GLTFLoader();
var mixer;
var model;

loader.load(
    `${assetUrl}`, function (gltf) {
        console.log('ffffff')
        gltf.scene.traverse(function (node) {
            if (node instanceof THREE.Mesh) {
                node.castShadow = true;
                node.material.side = THREE.DoubleSide;
            }
        });


        model = gltf.scene;
      //  model.scale.set(.35, .35, .35);
        model.position.set(0, 1.5, 0)
        scene.add(model);
        
        mixer = new THREE.AnimationMixer(model);
        mixer.clipAction(gltf.animations[1]).play();



});

   

var clock = new THREE.Clock();
function render() {
    requestAnimationFrame(render);
    var delta = clock.getDelta();
    if (mixer != null) mixer.update(delta);
    //if (model) model.rotation.y += 0.025;

    renderer.render(scene, camera);
}

render();
//gsap.registerPlugin(ScrollTrigger);
//let scrollingTL = gsap.timeline({
//    scrollTrigger: {
//        trigger: renderer.domElement,
//        start: 'top top',
//        end: '+=500%',
//        pin: true,
//        scrub: true,
//        onUpdate: function () {
//            camera.updateProjectionMatrix();
//        }
//    },
//})

//scrollingTL
//    .to(camera.position, {
//        x: 3,
//        ease: 'ease:Circ.easeOut',
//    }, 0)


//=====================================================
function animate() {
    requestAnimationFrame(animate);
    controls.update();
    update();
    renderer.render(scene, camera);
}
animate();