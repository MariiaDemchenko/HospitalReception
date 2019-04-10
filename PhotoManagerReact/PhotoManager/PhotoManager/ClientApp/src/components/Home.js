import React from 'react';
import Gallery from 'react-grid-gallery';
import { connect } from 'react-redux';

const images =
    [{
        src: "http://localhost/images/black.jpg",
        thumbnail: "http://localhost/thumbs/black.jpg",
        thumbnailWidth: 320,
        thumbnailHeight: 174,
        caption: "Foggy Lake Photo"
    },
    {
        src: "http://localhost/images/blue.jpg",
        thumbnail: "http://localhost/thumbs/blue.jpg",
        thumbnailWidth: 320,
        thumbnailHeight: 212,
        caption: "Strawberry on blue surface"
    },

    {
        src: "http://localhost/images/yellow.jpg",
        thumbnail: "http://localhost/thumbs/yellow.jpg",
        thumbnailWidth: 320,
        thumbnailHeight: 212
    },

    {
        src: "http://localhost/images/brown.jpg",
        thumbnail: "http://localhost/images/brown.jpg",
        thumbnailWidth: 320,
        thumbnailHeight: 212
    },

    {
        src: "http://localhost/images/clearLake.jpg",
        thumbnail: "http://localhost/images/clearLake.jpg",
        thumbnailWidth: 320,
        thumbnailHeight: 212
    },

    {
        src: "http://localhost/images/coffee.jpg",
        thumbnail: "http://localhost/images/coffee.jpg",
        thumbnailWidth: 320,
        thumbnailHeight: 212
    },

    {
        src: "http://localhost/images/colors.jpg",
        thumbnail: "http://localhost/images/colors.jpg",
        thumbnailWidth: 320,
        thumbnailHeight: 212
    },

    {
        src: "http://localhost/images/darkblue.jpg",
        thumbnail: "http://localhost/images/darkblue.jpg",
        thumbnailWidth: 320,
        thumbnailHeight: 212
    },

    {
        src: "http://localhost/images/desert.jpg",
        thumbnail: "http://localhost/images/desert.jpg",
        thumbnailWidth: 320,
        thumbnailHeight: 212
    },

    {
        src: "http://localhost/images/dessert.jpg",
        thumbnail: "http://localhost/images/dessert.jpg",
        thumbnailWidth: 320,
        thumbnailHeight: 212
    }
        ,

    {
        src: "http://localhost/images/forest.jpg",
        thumbnail: "http://localhost/images/forest.jpg",
        thumbnailWidth: 320,
        thumbnailHeight: 212
    },

    {
        src: "http://localhost/images/gray.jpg",
        thumbnail: "http://localhost/images/gray.jpg",
        thumbnailWidth: 320,
        thumbnailHeight: 212
    },

    {
        src: "http://localhost/images/green.jpg",
        thumbnail: "http://localhost/images/green.jpg",
        thumbnailWidth: 320,
        thumbnailHeight: 212
    }
    ]

const Home = props => (
    <div>
        <h1>Photos</h1>
        <Gallery images={images} />        
    </div>

);

export default connect()(Home);