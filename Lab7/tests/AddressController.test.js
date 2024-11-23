const request = require('supertest');
const app = require('../app'); /

describe('AddressController Integration Tests', () => {
    it('should return all addresses (GET /api/Address)', async () => {
        const response = await request(app).get('/api/Address');
        expect(response.statusCode).toBe(200);
        expect(Array.isArray(response.body)).toBeTruthy();
    });

    it('should return a specific address (GET /api/Address/:id)', async () => {
        const id = 1;
        const response = await request(app).get(`/api/Address/${id}`);
        expect(response.statusCode).toBe(200);
        expect(response.body).toHaveProperty('id', id);
    });

    it('should return 404 for non-existent address (GET /api/Address/:id)', async () => {
        const invalidId = 9999;
        const response = await request(app).get(`/api/Address/${invalidId}`);
        expect(response.statusCode).toBe(404);
    });
});
