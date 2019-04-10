import React from 'react';
import Gallery from 'react-grid-gallery';
import { connect } from 'react-redux';

const images =
    [{
        src: "http://mdemchenko.vrn.dataart.net/images/black.jpg",
        thumbnail: "http://mdemchenko.vrn.dataart.net/thumbs/black.jpg",
        thumbnailWidth: 320,
        thumbnailHeight: 174,
        caption: "Foggy Lake Photo"
    },
    {
        src: "http://mdemchenko.vrn.dataart.net/images/blue.jpg",
        thumbnail: "http://mdemchenko.vrn.dataart.net/thumbs/blue.jpg",
        thumbnailWidth: 320,
        thumbnailHeight: 212,
        caption: "Strawberry on blue surface"
    },

    {
        src: "http://mdemchenko.vrn.dataart.net/images/yellow.jpg",
        thumbnail: "http://mdemchenko.vrn.dataart.net/thumbs/yellow.jpg",
        thumbnailWidth: 320,
        thumbnailHeight: 212
    },

    {
        src: "http://mdemchenko.vrn.dataart.net/images/brown.jpg",
        thumbnail: "http://mdemchenko.vrn.dataart.net/images/brown.jpg",
        thumbnailWidth: 320,
        thumbnailHeight: 212
    },

    {
        src: "http://mdemchenko.vrn.dataart.net/images/clearLake.jpg",
        thumbnail: "http://mdemchenko.vrn.dataart.net/images/clearLake.jpg",
        thumbnailWidth: 320,
        thumbnailHeight: 212
    },

    {
        src: "http://mdemchenko.vrn.dataart.net/images/coffee.jpg",
        thumbnail: "http://mdemchenko.vrn.dataart.net/images/coffee.jpg",
        thumbnailWidth: 320,
        thumbnailHeight: 212
    },

    {
        src: "http://mdemchenko.vrn.dataart.net/images/colors.jpg",
        thumbnail: "http://mdemchenko.vrn.dataart.net/images/colors.jpg",
        thumbnailWidth: 320,
        thumbnailHeight: 212
    },

    {
        src: "http://mdemchenko.vrn.dataart.net/images/darkblue.jpg",
        thumbnail: "http://mdemchenko.vrn.dataart.net/images/darkblue.jpg",
        thumbnailWidth: 320,
        thumbnailHeight: 212
    },

    {
        src: "http://mdemchenko.vrn.dataart.net/images/desert.jpg",
        thumbnail: "http://mdemchenko.vrn.dataart.net/images/desert.jpg",
        thumbnailWidth: 320,
        thumbnailHeight: 212
    },

    {
        src: "http://mdemchenko.vrn.dataart.net/images/dessert.jpg",
        thumbnail: "http://mdemchenko.vrn.dataart.net/images/dessert.jpg",
        thumbnailWidth: 320,
        thumbnailHeight: 212
    }
        ,

    {
        src: "http://mdemchenko.vrn.dataart.net/images/forest.jpg",
        thumbnail: "http://mdemchenko.vrn.dataart.net/images/forest.jpg",
        thumbnailWidth: 320,
        thumbnailHeight: 212
    },

    {
        src: "http://mdemchenko.vrn.dataart.net/images/gray.jpg",
        thumbnail: "http://mdemchenko.vrn.dataart.net/images/gray.jpg",
        thumbnailWidth: 320,
        thumbnailHeight: 212
    },

    {
        src: "http://mdemchenko.vrn.dataart.net/images/green.jpg",
        thumbnail: "http://mdemchenko.vrn.dataart.net/images/green.jpg",
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