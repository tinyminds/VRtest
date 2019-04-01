using System.Collections;
using ProceduralToolkit.Examples.UI;
using UnityEngine;

namespace ProceduralToolkit.Examples
{
    public class BoidControllerConfigurator : ConfiguratorBase
    {
        public MeshFilter meshFilter;
        public BoidController.Config config = new BoidController.Config();

        private BoidController controller;
        private bool simulate = true;

        private void Awake()
		{
			Generate ();
			StartCoroutine (Simulate ());
		}

        private IEnumerator Simulate()
        {
            while (true)
            {
                yield return StartCoroutine(controller.CalculateVelocities());
            }
        }

        private void Update()
        {
            if (simulate)
            {
                controller.Update();
            }
            UpdateSkybox();
        }

        private void Generate()
        {
            controller = new BoidController();

            GeneratePalette();
            Color colorA = GetMainColorHSV().ToColor();
            Color colorB = GetSecondaryColorHSV().ToColor();

            config.template = MeshDraft.Tetrahedron(0.3f);
            // Assuming that we are dealing with tetrahedron, first vertex should be boid's "nose"
            config.template.colors.Add(colorA);
            for (int i = 1; i < config.template.vertexCount; i++)
            {
                config.template.colors.Add(colorB);
            }

            meshFilter.mesh = controller.Generate(config);
			//transform.gameObject.AddComponent<MeshCollider>();
			//transform.GetComponent<MeshCollider>().sharedMesh = meshFilter.mesh;
        }
    }
}