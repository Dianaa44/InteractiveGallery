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
controls.enableKeys = false;
controls.enableDamping = true; 
controls.dampingFactor = 0.1;
//controls.screenSpacePanning = false;
//controls.minDistance = 0.2;
//controls.maxDistance = 10;
//controls.target.set(0, 2, 0);
//const tt = new THREE.Vector3(0,2,0);
//controls.maxPolarAngle = Math.PI / 2;
//===================================================== scene
var scene = new THREE.Scene();
const raycaster = new THREE.Raycaster();
//===================================================== camera
//camera.position.z = 5;
//camera.position.y = 1.5;
// Set initial camera position
camera.position.set(0, 1, -7.5);
//camera.lookAt(0, 1,7.5 );
//===================================================== lights
//const light = new THREE.directionallight(0xefefff, 3);
//light.position.set(1, 1, 1).normalize();
//scene.add(light);
//const light2 = new THREE.PointLight(0xefefff, 1, 100);
//light2.position.set(1, 1, 1);
//scene.add(light2);
//var light = new three.directionallight(0xffefef, 3);
//light.position.set(1, 1, 1).normalize();
//scene.add(light);

//===================================================== resize

//window.addEventListener("resize", function () {
//    let width = window.innerWidth;
//    let height = window.innerHeight;
//    renderer.setSize(width, height);
//    camera.aspect = width / height;
//    camera.updateProjectionMatrix();
//});

var moveSpeed = 0.05;
var rotateSpeed = 0.02;

document.addEventListener('keydown', function (event) {
    switch (event.key) {
        case 'ArrowUp':
            camera.translateZ(-moveSpeed);
            break;
        case 'ArrowDown':
            camera.translateZ(moveSpeed);
            break;
        case 'ArrowLeft':
            camera.rotation.y += rotateSpeed;
            break;
        case 'ArrowRight':
            camera.rotation.y -= rotateSpeed;
            break;
    }
});



//const imageGeometry = new THREE.PlaneGeometry(1,1);
////// Load image texture
//const textureLoader = new THREE.TextureLoader();
//const imageTexture = textureLoader.load(`${image1}`);
//const imageMaterial = new THREE.MeshBasicMaterial({ map: imageTexture });
//const imagePlane = new THREE.Mesh(imageGeometry, imageMaterial);
//imagePlane.position.set(-2,2,-7.4);
//scene.add(imagePlane);
var imagesNum = 0;
function addImage(image) {
    const imageGeometry = new THREE.PlaneGeometry(1, 1);
    //// Load image texture
    const textureLoader = new THREE.TextureLoader();
    const imageTexture = textureLoader.load(image);
    const imageMaterial = new THREE.MeshBasicMaterial({ map: imageTexture });
    const imagePlane = new THREE.Mesh(imageGeometry, imageMaterial);
    imagesNum += 1;
    var p = new THREE.Vector3();
    p=findImagePosition();
    console.log(p);
    imagePlane.position.set(p.x,p.y,p.z);
    console.log(imagePlane.position);
    if (imagesNum<=12 && imagesNum % 3 == 1) imagePlane.rotateY(Math.PI);
    else if ((imagesNum <= 12 && imagesNum % 3 == 2) || (imagesNum > 12 && imagesNum % 2 == 1)) imagePlane.rotateY(-Math.PI / 2);
    else imagePlane.rotateY(Math.PI / 2);
    console.log(imagePlane.position)
    scene.add(imagePlane);
}

for (var i = 0; i<16; i++) {
    addImage(`${image1}`)
}
function findImagePosition() {
    if (imagesNum <= 12) {
        if (imagesNum % 3 == 1) {
            var ix = (Math.floor(imagesNum / 3) + 1) * 2-5;
            return new THREE.Vector3(ix,2,7.45);
        }
        else if (imagesNum % 3 == 2) {
            var iz = (Math.floor(imagesNum / 3) + 1) * 2 - 7;
            return new THREE.Vector3(4.95, 2, iz);
        }
        else {
            var iz = (Math.floor(imagesNum / 3) + 1) * 2 - 10;
            return new THREE.Vector3(-4.95, 2, iz);
        }
    }
    else if (imagesNum <= 16) {
        if (imagesNum % 2 == 1) {
            var iz = (Math.floor((imagesNum-12) / 2) + 5) * 2 - 7;
            return new THREE.Vector3(4.95, 2, iz);
        }
        else {
            var iz = (Math.floor((imagesNum - 12) / 2) + 5) * 2 - 10;
            return new THREE.Vector3(-4.95, 2, iz);
        }
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
        model.position.set(0, 1, 0)
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
    renderer.render(scene, camera);
}
animate();