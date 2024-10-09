import { useCallback, useState } from "react";
import type { Container, Engine } from "tsparticles-engine";
import Particles from "react-tsparticles";
import { loadSlim } from "tsparticles-slim";
import logo from '../../assets/img/logo.png';
import './Dashboard.css';
import 'bootstrap/dist/css/bootstrap.min.css';
import '@fortawesome/fontawesome-free/css/all.css';

const Dashboard = () => {
    const [alreadyLogin, setAlreadyLogin] = useState(false);

    const particlesInit = useCallback(async (engine: Engine) => {

        await loadSlim(engine);
    }, []);

    const particlesLoaded = useCallback(async (container: Container | undefined) => {
        await console.log(container);
    }, []);
    return (
        <>
            <Particles
                id="tsparticles"
                init={particlesInit}
                loaded={particlesLoaded}
                options={{
                    background: {
                        color: {
                            value: "#207cca",
                        },
                    },
                    fpsLimit: 120,
                    particles: {
                        color: {
                            value: "#ffffff",
                        },
                        links: {
                            color: "#ffffff",
                            distance: 150,
                            enable: true,
                            opacity: 0.5,
                            width: 1,
                        },
                        move: {
                            direction: "none",
                            enable: true,
                            outModes: {
                                default: "bounce",
                            },
                            random: true,
                            speed: 2,
                            straight: true,
                        },
                        number: {
                            value: 150,
                            density: {
                                enable: true,
                                value_area: 1000,
                            }
                        },
                        shape: {
                            type: "circle",
                        },
                        size: {
                            value: 1,
                            random: true,
                        },
                    },
                    detectRetina: true,
                }}
            />

            <div className="container" style={{ position: 'relative' }}>
                <div className="row">
                    <div className="col-12 d-flex justify-content-end align-items-center mb-3">
                        <div className="navigation-right">
                            <i className="icofont-phone" /> +91-989-977-4341
                            <i className="icofont-clock-time icofont-rotate-180" /> Wed-Sun: Opening at 6:30 AM
                        </div>
                    </div>
                </div>

                <div className="row">
                    <div className="col-12 d-flex justify-content-center align-items-center mb-3">
                        <div className="elementor-image">
                            <img src={logo} alt="Logo" />
                        </div>
                    </div>
                </div>
                <div className="row text-center mb-4">
                    <div className="col-12 d-flex justify-content-center">
                        <div className="elementor-heading">
                            <h2 className="fadeInDown" data-text="Transform Learning into Employment...!">
                                Transform Learning into Employment...!
                            </h2>
                        </div>
                    </div>
                </div>
                <div className="row form-background">
                    <div className="dashoard-continer">

                    </div>
                </div>
                <div className="row">
                    <div className="col-12 d-flex justify-content-center">
                        <div className="social-links d-flex justify-content-around">
                            <a href="https://twitter.com/stepuplogics" target="_blank" rel="noreferrer" className="twitter mx-2">
                                <i className="fab fa-twitter" />
                            </a>
                            <a href="https://www.facebook.com/stepuplogics/" target="_blank" rel="noreferrer" className="facebook mx-2">
                                <i className="fab fa-facebook-f" />
                            </a>
                            <a href="https://www.instagram.com/stepuplogics/" target="_blank" rel="noreferrer" className="instagram mx-2">
                                <i className="fab fa-instagram" />
                            </a>
                            <a href="https://www.kooapp.com/profile/stepuplogics" target="_blank" rel="noreferrer" className="google-plus mx-2">
                                <i className="fab fa-google-plus-g" />
                            </a>
                            <a href="https://www.linkedin.com/company/stepup-logics" target="_blank" rel="noreferrer" className="linkedin mx-2">
                                <i className="fab fa-linkedin-in" />
                            </a>
                        </div>
                    </div>
                </div>
            </div>

        </>
    );
};

export default Dashboard;